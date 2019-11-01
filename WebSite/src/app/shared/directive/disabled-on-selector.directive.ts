import { Directive, Input, OnChanges, ElementRef, Renderer2 } from '@angular/core';

@Directive({
    selector: '[appNgxDisabledOnSelector]'
})
export class DisabledOnSelectorDirective implements OnChanges {
    @Input() appNgxDisabledOnSelector: string;
    @Input() disabled: boolean;
    private readonly nativeElement: HTMLElement;

    constructor(
        private el: ElementRef,
        private renderer2: Renderer2) {
        this.nativeElement = el.nativeElement;
    }

    ngOnChanges() {
        setTimeout(() => {
            const elements = this.nativeElement.querySelectorAll(this.appNgxDisabledOnSelector);
            for (let i = 0; i < elements.length; i++) {
                this.renderer2.setProperty(elements[i], 'disabled', this.disabled);
            }
        });
    }
}
