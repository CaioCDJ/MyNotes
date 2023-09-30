import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { EditorInstance, EditorOption } from 'angular-markdown-editor';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MarkdownService } from 'ngx-markdown';

@Component({
  selector: 'app-note',
  encapsulation: ViewEncapsulation.None,
  templateUrl: './note.component.html',
  styleUrls: ['./note.component.css'],
})
export class NoteComponent implements OnInit {
  bsEditorInstance!: EditorInstance;
  markdownText!: string;
  editorOptions!: EditorOption;
  templateForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private markdownService: MarkdownService
  ) { }

  ngOnInit() {
    this.editorOptions = {
      autofocus: false,
      iconlibrary: 'fa',
      savable: false,
      onFullscreenExit: (e) => this.hidePreview(),
      onShow: (e) => (this.bsEditorInstance = e),
      parser: (val) => this.parse(val),
    };
    this.markdownText = `
# PEnes kek \n
NAO FAZ SENTIDO

- KDKAKDKSAD

`;
    this.buildForm(this.markdownText);
    this.onFormChanges();
  }

  buildForm(markdownText: string) {
    this.templateForm = this.fb.group({
      body: [markdownText],
      isPreview: [true],
    });
  }

  /** highlight all code found, needs to be wrapped in timer to work properly */
  highlight() {
    setTimeout(() => {
      this.markdownService.highlight();
    });
  }

  hidePreview() {
    if (this.bsEditorInstance && this.bsEditorInstance.hidePreview) {
      this.bsEditorInstance.hidePreview();
    }
  }

  showFullScreen(isFullScreen: boolean) {
    if (this.bsEditorInstance && this.bsEditorInstance.setFullscreen) {
      this.bsEditorInstance.showPreview();
      this.bsEditorInstance.setFullscreen(isFullScreen);
    }
  }

  parse(inputValue: string) {
    const markedOutput = this.markdownService.parse(inputValue.trim());
    this.highlight();

    return markedOutput;
  }

  onFormChanges(): void {
    this.templateForm.valueChanges.subscribe((formData) => {
      if (formData) {
        this.markdownText = formData.body;
      }
    });
  }
}
